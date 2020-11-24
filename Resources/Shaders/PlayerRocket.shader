shader_type canvas_item;
uniform sampler2D diffuse;

void fragment() {
    vec4 col = texture(diffuse, UV).rgba;
    col.r = 0.0;
    col.g = 0.0;
    col.b = 0.0;
    if (UV.x < 0.32 && UV.y < 0.72) {
        col.a = 0.0;
    } else {
        col.a = 0.7;
    }
    COLOR = col;
}