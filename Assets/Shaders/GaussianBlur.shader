Shader "CRLuo/CRLuo_Teaching62_GrabPass_FlowerGlass"
{
	Properties
	{ [Header(BaseColor)]
		_GlassColor("玻璃颜色", Color) = (1, 1, 1, 1)

		_GlassMaskTex("玻璃颜色范围（A）磨砂范围",2D) = "black"{}
		_GlassColorR("红色范围颜色", Color) = (1, 0, 0, 1)
		_GlassColorG("绿色范围颜色", Color) = (0,1, 0, 1)
		_GlassColorB("蓝色范围颜色", Color) = (0, 0, 1, 1)
					[Header(Bump)]
		[NoScaleOffset]
		_BumpMap("法线贴图",2D) = "bump"{}
		_BumpScale("法线贴图强度",Range(0,2.0)) = -1.0
		[Header(Highlight)]
		_HighlightColor("高光颜色", Color) = (1, 1, 1, 1)
		_HighlightPow("高光范围",  Range(0.001,1)) = 0.1
				[Header(Reflection)]
		_ReflectionRate("反射率",  Range(0.001,1)) = 0.1
		_ReflectionBlur("反射模糊",  Range(0.001,1)) = 0.1
				[NoScaleOffset]
		_CubeMap("环境球贴图",cube) = "Skybox"{}
				[Header(Refraction)]
		_RefractionRate("折射率",  Range(-1,1)) = 0.1
		_RefractionBlur("折射模糊",  Range(0,1)) = 0.1


	}
		SubShader
		{
			Tags
			{
				"Queue" = "Transparent"
			}

			//预先渲染
			GrabPass{}

			Pass
			{
				// Transparent "normal" blending.
				//Blend SrcAlpha OneMinusSrcAlpha
				//ZWrite Off

				CGPROGRAM
				#define SMOOTHSTEP_AA 0.1

				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"


				struct appdata
				{
					float4 vertex : POSITION;
					float4 uv : TEXCOORD0;
					float3 normal : NORMAL;
					float4 tangent:TANGENT;
				};

				struct v2f
				{
					float4 vertex : SV_POSITION;
					float2 uv : TEXCOORD0;
					float2 distortUV : TEXCOORD1;
					float4 screenPos : TEXCOORD2;
					float3 viewNormal : NORMAL;
					//切线空间转世界空间数据3代4
						float4 T2W0:TEXCOORD3;
						float4 T2W1:TEXCOORD4;
						float4 T2W2:TEXCOORD5;

						float3	TtoV0 : TEXCOORD6;
						float3	TtoV1 : TEXCOORD7;
						float3	TtoV2 : TEXCOORD8;
				};

				float _HighlightPow;
				float4 _GlassColor;
							float4 _GlassColorR;
				float4 _GlassColorG;
				float4 _GlassColorB;
				sampler2D _GlassMaskTex;
				float4 _GlassMaskTex_ST;
				float _RefractionBlur;
				float _WaterDownRef;
				float _RefractionRate;
				float _ReflectionRate;
				float _ReflectionBlur;

				//场景预渲染
				sampler2D _GrabTexture : register(s0);

				//定义法线贴图变量
				sampler2D _BumpMap;
				float4 _BumpMap_ST;
				//定义法线强度变量
				float _BumpScale;
				float4 _HighlightColor;
				//获取环境球贴图
samplerCUBE _CubeMap;
v2f vert(appdata v)
{
	v2f o;
	//o.distortUV = TRANSFORM_TEX(v.uv, _SurfaceDistortion);
	o.uv = TRANSFORM_TEX(v.uv, _GlassMaskTex);
   o.vertex = UnityObjectToClipPos(v.vertex);
	o.screenPos = ComputeScreenPos(o.vertex);

	o.viewNormal = COMPUTE_VIEW_NORMAL;
	//世界法线
		float3 worldNormal = UnityObjectToWorldNormal(v.normal);
		//世界切线
		float3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
		//世界副切线
		float3 worldBinormal = cross(worldNormal,worldTangent) * v.tangent.w;
		//世界坐标
		float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;

		//构建变换矩阵
		//z轴是法线方向(n)，x轴是切线方向(t)，y轴可由法线和切线叉积得到，也称为副切线（bitangent, b）
		o.T2W0 = float4(worldTangent.x,worldBinormal.x,worldNormal.x, worldPos.x);
		o.T2W1 = float4(worldTangent.y,worldBinormal.y,worldNormal.y, worldPos.y);
		o.T2W2 = float4(worldTangent.z,worldBinormal.z,worldNormal.z, worldPos.z);

		//获取模型到切线转换变量  rotation
			TANGENT_SPACE_ROTATION;


			//转递切线到视角的转换变量组
			o.TtoV0 = normalize(mul(rotation, UNITY_MATRIX_IT_MV[0].xyz));
			o.TtoV1 = normalize(mul(rotation, UNITY_MATRIX_IT_MV[1].xyz));
			o.TtoV2 = normalize(mul(rotation, UNITY_MATRIX_IT_MV[2].xyz));


	return o;
}



float4 frag(v2f i) : SV_Target
{

	//获取法线贴图
			float4 Normaltex = tex2D(_BumpMap, i.uv);
			//法线贴图0~1转-1~1
			float3 tangentNormal = UnpackNormal(Normaltex);
			//乘以凹凸系数
			tangentNormal.xy *= _BumpScale;
			//向量点乘自身算出x2+y2，再求出z的值
			tangentNormal.z = sqrt(1.0 - saturate(dot(tangentNormal.xy, tangentNormal.xy)));
			//向量变换只需要3*3
			float3x3 T2WMatrix = float3x3(i.T2W0.xyz,i.T2W1.xyz,i.T2W2.xyz);
			//法线从切线空间到世界空间
			float3 worldNormal = mul(T2WMatrix,tangentNormal);

			worldNormal = normalize(worldNormal);


			//切线空间转视角空间 向量变换只需要3*3
				float3x3 TtoVMatrix = float3x3(i.TtoV0.xyz,i.TtoV1.xyz,i.TtoV2.xyz);
				//法线从切线空间到视角空间
				float3 _viewNormal = mul(TtoVMatrix,tangentNormal);

				//获取顶点世界坐标
				float3 WordPos = float3(i.T2W0.w, i.T2W1.w, i.T2W2.w);
				//摄像机方向
				fixed3 viewDir = normalize(_WorldSpaceCameraPos.xyz - WordPos);
				//世界坐标灯光方向
				fixed3 lightDir = normalize(_WorldSpaceLightPos0.xyz);

				//使用反向光源方向与法线求高光反射
									fixed3 reflectDir = normalize(reflect(-lightDir, worldNormal));
										fixed3 reflMapDir = normalize(reflect(-viewDir, worldNormal));
										//反射光线与视角点乘
									fixed 	Ramp_Specular = saturate(dot(reflectDir,viewDir));

									Ramp_Specular = pow(Ramp_Specular,1000 * _HighlightPow);

									fixed Ramp_Face = saturate(dot(worldNormal,viewDir));

									//模糊的原理就是错开uv来采样，实现模糊
											  float2 screenPos = i.screenPos.xy / i.screenPos.w;
											  float offsetSize = 0.002 * _RefractionBlur;
											  _RefractionRate = Ramp_Face * _RefractionRate * 2;
											  half4 refractionTexAdd = half4(0.0h,0.0h,0.0h,0.0h);
											  refractionTexAdd += tex2D(_GrabTexture, float2(screenPos.x + 5.0 * offsetSize, screenPos.y - 3.0 * offsetSize) + _viewNormal.xy * _RefractionRate) * 0.025;

											  refractionTexAdd += tex2D(_GrabTexture, float2(screenPos.x - 4.0 * offsetSize, screenPos.y + 2.5 * offsetSize) + _viewNormal.xy * _RefractionRate) * 0.05;
											  refractionTexAdd += tex2D(_GrabTexture, float2(screenPos.x + 4.0 * offsetSize, screenPos.y - 2.5 * offsetSize) + _viewNormal.xy * _RefractionRate) * 0.05;


											  refractionTexAdd += tex2D(_GrabTexture, float2(screenPos.x - 3.0 * offsetSize, screenPos.y + 2.0 * offsetSize) + _viewNormal.xy * _RefractionRate) * 0.09;
											  refractionTexAdd += tex2D(_GrabTexture, float2(screenPos.x + 3.0 * offsetSize, screenPos.y - 2.0 * offsetSize) + _viewNormal.xy * _RefractionRate) * 0.09;

											  refractionTexAdd += tex2D(_GrabTexture, float2(screenPos.x - 2.0 * offsetSize, screenPos.y + 1.5 * offsetSize) + _viewNormal.xy * _RefractionRate) * 0.12;
											  refractionTexAdd += tex2D(_GrabTexture, float2(screenPos.x + 2.0 * offsetSize, screenPos.y - 1.5 * offsetSize) + _viewNormal.xy * _RefractionRate) * 0.12;

											  refractionTexAdd += tex2D(_GrabTexture, float2(screenPos.x - 1.0 * offsetSize, screenPos.y + 1.0 * offsetSize) + _viewNormal.xy * _RefractionRate) * 0.15;
											  refractionTexAdd += tex2D(_GrabTexture, float2(screenPos.x + 1.0 * offsetSize, screenPos.y - 1.0 * offsetSize) + _viewNormal.xy * _RefractionRate) * 0.15;

											  //红偏色
											  refractionTexAdd += tex2D(_GrabTexture, screenPos - 3.0 * offsetSize + _viewNormal.xy * _RefractionRate) * 0.5;


											  refractionTexAdd += tex2D(_GrabTexture, screenPos - 2.5 * offsetSize + _viewNormal.xy * _RefractionRate) * 0.05;
											  refractionTexAdd += tex2D(_GrabTexture, screenPos - 2.0 * offsetSize + _viewNormal.xy * _RefractionRate) * 0.09;
											  refractionTexAdd += tex2D(_GrabTexture, screenPos - 1.5 * offsetSize + _viewNormal.xy * _RefractionRate) * 0.12;
											  refractionTexAdd += tex2D(_GrabTexture, screenPos - 1.0 * offsetSize + _viewNormal.xy * _RefractionRate) * 0.15;
											  //绿偏色
											  float4 Old_refraction = tex2D(_GrabTexture, screenPos + _viewNormal.xy * _RefractionRate);

											   refractionTexAdd += Old_refraction * 0.5;
											   //蓝偏色
											   refractionTexAdd += tex2D(_GrabTexture, screenPos + 3.0 * offsetSize + _viewNormal.xy * _RefractionRate) * 0.5;
											   refractionTexAdd += tex2D(_GrabTexture, screenPos + 2.5 * offsetSize + _viewNormal.xy * _RefractionRate) * 0.12;
											   refractionTexAdd += tex2D(_GrabTexture, screenPos + 2.0 * offsetSize + _viewNormal.xy * _RefractionRate) * 0.09;
											   refractionTexAdd += tex2D(_GrabTexture, screenPos + 1.5 * offsetSize + _viewNormal.xy * _RefractionRate) * 0.05;
											   refractionTexAdd += tex2D(_GrabTexture, screenPos + 1.0 * offsetSize + _viewNormal.xy * _RefractionRate) * 0.025;

											   //多清晰度读取环境求
											   float3 _CubeMapColor = texCUBElod(_CubeMap, fixed4(reflMapDir, _ReflectionBlur * 8)) * _ReflectionRate;
											  float4 OutLineColor = pow((1 - Ramp_Face),(_ReflectionBlur + 1)) * _HighlightColor * 0.5;
											  float4  glassMask = tex2D(_GlassMaskTex,i.uv);

											  float4  glassCol = _GlassColor;
											  glassCol = lerp(glassCol, _GlassColorR * glassMask.r + _GlassColorG * glassMask.g + _GlassColorB * glassMask.b,glassMask.r + glassMask.g + glassMask.b);
											   float4 OutColor = glassCol;
											  glassCol = lerp(float4(0,0,0,1), pow(glassCol,pow((1 - Ramp_Face) * 2,3)) * glassCol,Ramp_Face);

											  refractionTexAdd = lerp(Old_refraction * 2,refractionTexAdd,glassMask.a);


											 OutColor.rgb = refractionTexAdd * glassCol.rgb + _CubeMapColor * (1 - Ramp_Face) * _HighlightColor;


											OutColor += +Ramp_Specular * _HighlightColor * _HighlightColor.a + OutLineColor * _ReflectionRate;

											//  OutColor.a = lerp(OutColor.a ,1,glassCol.a);
													return OutColor;
												}
												ENDCG
											}
		}
}