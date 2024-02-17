public static class MapValue
{
    public static float Calculation(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        // İlk aralığı [fromMin, fromMax] aralığından [toMin, toMax] aralığına dönüştür
        return toMin + (value - fromMin) * (toMax - toMin) / (fromMax - fromMin);

    }
}