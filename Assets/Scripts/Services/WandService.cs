using System.Collections.Generic;
using System.Threading.Tasks;
using PDollarGestureRecognizer;
using UnityEngine;

namespace HarryPoter.Core
{
    public class WandService : IService
    {
        private const string CIRCLE_SPELL = "Circle";
        private const string TRIANGLE_SPELL = "Triangle";
        private const string INFINITY_SPELL = "Infinity";
        
        public Wand CurrentWand { get; private set; }
        public WandConfiguration Configuration { get; private set; }

        private List<Gesture> _gestures = new List<Gesture>();

        public WandService(WandConfiguration configuration)
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

        public void Recognize(List<Point> points)
        {
            if (Configuration.IsWritingNewSpells)
            {
                WriteNewSpell(points);
                return;
            }
            
            ActivateSpell(points);
        }

        public void SetWand(Wand wand)
        {
            if (CurrentWand != null)
            {
                Object.Destroy(CurrentWand.gameObject);
                CurrentWand = null;
            }
            
            CurrentWand = wand;
        }

        private int temp = 0;
        private void WriteNewSpell(List<Point> points)
        {
            GestureIO.WriteGesture(points.ToArray(), Configuration.SpellName, Application.persistentDataPath + $"Spell_{temp}.xml");
            temp++;
        }

        private void ActivateSpell(List<Point> points)
        {
            if (CurrentWand == null)
            {
                return;
            }
            
            Gesture candidate = new Gesture(points.ToArray());
            Result result = PointCloudRecognizer.Classify(candidate, _gestures.ToArray());

            switch (result.GestureClass)
            {
                case CIRCLE_SPELL:
                    CurrentWand.WandSpell.ActivateSpell(WandSpell.ESpell.Open);
                    break;
                case INFINITY_SPELL:
                    CurrentWand.WandSpell.ActivateSpell(WandSpell.ESpell.Take);
                    break;
                case TRIANGLE_SPELL:
                    CurrentWand.WandSpell.ActivateSpell(WandSpell.ESpell.Attack);
                    break;
                default:
                    Debug.Log("Can't recognize SPELL!");
                    break;
            }
        }
    }
}