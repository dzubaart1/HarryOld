using System.Collections.Generic;
using System.Threading.Tasks;
using PDollarGestureRecognizer;

namespace HarryPoter.Core
{
    public class PointingService : IService
    {
        public PointingConfiguration Configuration { get; private set; }

        private List<Gesture> _gestures = new List<Gesture>();

        public PointingService(PointingConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public Task Initialize()
        {
            foreach (var gestureTextAsset in Configuration.GestureTextAssets)
            {
                _gestures.Add(GestureIO.ReadGestureFromXML(gestureTextAsset.text));
            }
            
            return Task.CompletedTask;
        }

        public void Destroy()
        {
        }

        public Result Recognize(List<Point> points)
        {
            Gesture candidate = new Gesture(points.ToArray());
            return PointCloudRecognizer.Classify(candidate, _gestures.ToArray());
        }
    }
}