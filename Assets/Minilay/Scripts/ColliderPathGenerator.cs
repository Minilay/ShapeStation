using System.Collections.Generic;
using UnityEngine;

namespace Minilay.ShapeStation.Scripts
{
    public static class ColliderPathGenerator
    {

        public static List<Vector2> GeneratePath(ShapeProperties shapeProperties)
        {
            var outsidePoints = new List<Vector2>();
            var insidePoints = new List<Vector2>();

            var vertexCount = shapeProperties.spikeValue == 0
                ? shapeProperties.vertexCount
                : shapeProperties.vertexCount / 2 + shapeProperties.vertexCount % 2;

            for (var i = 0; i < vertexCount; i++)
            {
                var angleStep = 360.0f / vertexCount;

                var angle = i * angleStep + shapeProperties.phase;
                var outerVertCoordinate = Utils.PolarToCartesian(angle) *
                                          (shapeProperties.radius * (1 + shapeProperties.spikeValue));
                var innerVertCoordinate = Utils.PolarToCartesian(angle + angleStep / 2) *
                                          (shapeProperties.radius * (1 - shapeProperties.spikeValue));

                outsidePoints.Add(outerVertCoordinate);
                insidePoints.Add(outerVertCoordinate * shapeProperties.holeSize);

                if (shapeProperties.spikeValue > 0)
                {
                    outsidePoints.Add(innerVertCoordinate);
                    insidePoints.Add(innerVertCoordinate * shapeProperties.holeSize);
                }
            }

            outsidePoints.Add(outsidePoints[0]);
            insidePoints.Add(insidePoints[0]);

            if (shapeProperties.holeSize > 0)
                outsidePoints.AddRange(insidePoints);

            return outsidePoints;
        }
    }
}