using System.Collections.Generic;
using HarryPoter.Core;
using UnityEngine;

public class Snitch : MonoBehaviour, ISpellable
{
    [SerializeField] private List<Transform> _points;
    [SerializeField] private GameObject _earPrefab;

    private const float ACCURACY = 0.1f;

    private ParticlesService _particlesService;

    private Transform _spawnPoint;
    private int _currentPoint;

    private void Awake()
    {
        _spawnPoint = Engine.GetService<InputService>().Player.SpawnPoint;
        _particlesService = Engine.GetService<ParticlesService>();
    }

    private void Start()
    {
        transform.position = _points[_currentPoint].position;
    }

    private void Update()
    {
        if ((transform.position - _points[_currentPoint].position).sqrMagnitude < ACCURACY)
        {
            _currentPoint = (_currentPoint + 1) % _points.Count;
        }

        transform.position = Vector3.MoveTowards(transform.position, _points[_currentPoint].position, Time.deltaTime * 0.1f);
        transform.rotation = Quaternion.Lerp(transform.rotation,
            Quaternion.LookRotation(_points[_currentPoint].position - transform.position), Time.deltaTime*5);
    }

    public void OnOpenSpell()
    {
    }

    public void OnAttackSpell()
    {
        _particlesService.SpawnParticlesSystem(ParticlesConfiguration.EParticle.QuestComplete, transform.position).Play();
        
        Instantiate(_earPrefab, _spawnPoint.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void OnTakeSpell()
    {
    }
}
