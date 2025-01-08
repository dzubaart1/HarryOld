using UnityEngine;

namespace Utils
{
    public static class PlaneExtensions
    {
        /// <summary>
        /// Проецирует точку из пространства (Vector3) на произвольную плоскость (Plane) и возвращает 2D-координаты.
        /// </summary>
        public static Vector2 ProjectToPlane(this Plane plane, Vector3 point)
        {
            // Получаем нормаль и точку на плоскости
            Vector3 planeNormal = plane.normal;
            Vector3 planePoint = plane.normal * -plane.distance;

            // Вычисляем базис плоскости
            Vector3 tangent = Vector3.Cross(GetArbitraryVector(planeNormal), planeNormal).normalized;
            Vector3 bitangent = Vector3.Cross(planeNormal, tangent).normalized;

            // Проецируем точку на плоскость
            Vector3 projectedPoint = plane.ClosestPointOnPlane(point);

            // Переводим проекцию в локальные координаты
            float x = Vector3.Dot(projectedPoint - planePoint, tangent); // Координата X
            float y = Vector3.Dot(projectedPoint - planePoint, bitangent); // Координата Y

            return new Vector2(x, y);
        }

        /// <summary>
        /// Преобразует 2D-координаты (Vector2) обратно в мировые координаты (Vector3) на плоскости.
        /// </summary>
        public static Vector3 ProjectToWorld(this Plane plane, Vector2 point2D)
        {
            // Получаем нормаль и точку на плоскости
            Vector3 planeNormal = plane.normal;
            Vector3 planePoint = plane.normal * -plane.distance;

            // Вычисляем базис плоскости
            Vector3 tangent = Vector3.Cross(GetArbitraryVector(planeNormal), planeNormal).normalized;
            Vector3 bitangent = Vector3.Cross(planeNormal, tangent).normalized;

            // Переводим из 2D-координат в мировые координаты
            Vector3 worldPoint = planePoint + point2D.x * tangent + point2D.y * bitangent;

            return worldPoint;
        }

        /// <summary>
        /// Выбирает вспомогательный вектор, не параллельный нормали плоскости.
        /// </summary>
        private static Vector3 GetArbitraryVector(Vector3 normal)
        {
            // Выбираем любой вектор, не коллинеарный нормали
            if (Mathf.Abs(normal.x) > Mathf.Abs(normal.y))
            {
                return Vector3.up; // Используем Vector3.up
            }

            return Vector3.right; // Используем Vector3.right
        }
    }
}