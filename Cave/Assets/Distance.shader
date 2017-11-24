Shader "Unlit/Distance"
{
	Properties
	{
		 _MainTex ("Base (RGB)", 2D) = "white" {}
         _MinColor ("Color in Minimal", Color) = (1, 0, 0, 1)
		 _MidColor("Color in Mid", Color) = (1, 1, 0, 1)
         _MaxColor ("Color in Maxmal", Color) = (0, 1, 0, 1)
         _MinDistance ("Min Distance", Float) = 10
		 _MidDistance ("Mid Distance", Float) = 50
         _MaxDistance ("Max Distance", Float) = 100
	}
	 SubShader {
         Tags { "RenderType"="Opaque" }
         LOD 200
         
         CGPROGRAM
         #pragma surface surf Lambert
 
         sampler2D _MainTex;
 
         struct Input {
             float2 uv_MainTex;
             float3 worldPos;
         };
 
         float _MaxDistance;
		 float _MidDistance;
         float _MinDistance;
         half4 _MinColor;
		 half4 _MidColor;
         half4 _MaxColor;
		half4 distanceColor;
         void surf (Input IN, inout SurfaceOutput o) {
             half4 c = tex2D (_MainTex, IN.uv_MainTex);
             float dist = distance(_WorldSpaceCameraPos, IN.worldPos);
             
			if(dist > _MidDistance){
				half weight = saturate( (dist - _MidDistance) / (_MaxDistance - _MidDistance) );
				distanceColor = lerp(_MidColor, _MaxColor, weight);
			}
			else{
				half weight = saturate( (dist - _MinDistance) / (_MaxDistance - _MinDistance) );
				distanceColor = lerp(_MinColor, _MidColor, weight);
			}
             o.Albedo = c.rgb * distanceColor.rgb;
             o.Alpha = c.a;
         }
         ENDCG
     } 
     FallBack "Diffuse"
}
