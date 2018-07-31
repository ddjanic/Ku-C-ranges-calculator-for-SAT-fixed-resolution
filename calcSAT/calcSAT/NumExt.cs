

using System;

namespace calcSAT
{
    // дополниельный метод (метод дополнения при вводе элементов и методов)
    public static class NumExt
    {
        // Конвертация градусов в радианы
        public static double ToRadians(this double val)
        {
            return (Math.PI / 180) * val;
        }
        // Конвертация радиан в градусы
        public static double ToDegrees(this double val)
        {
            return val * (180 / Math.PI);
        }

    }
}
