using UnityEngine;
using Asteroids.Model;
using System.Collections.Generic;

public class PresentersFactory : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Presenter _laserGunBulletTemplate;
    [SerializeField] private Presenter _defaultGunBulletTemplate;
    [SerializeField] private Presenter _asteroidTemplate;
    [SerializeField] private Presenter _asteroidPartTemplate;
    [SerializeField] private Presenter _nlo1Template;
    [SerializeField] private Presenter _nlo2Template;

    private List<Transformable> _nlo1list = new List<Transformable>();
    private List<Transformable> _nlo2list = new List<Transformable>();

    public void CreateBullet(Bullet bullet)
    {
        if(bullet is LaserGunBullet)
            CreatePresenter(_laserGunBulletTemplate, bullet);
        else
            CreatePresenter(_defaultGunBulletTemplate, bullet);
    }

    public void CreateAsteroidParts(AsteroidPresenter asteroid)
    {
        for (int i = 0; i < 4; i++)
            CreatePresenter(_asteroidPartTemplate, asteroid.Model.CreatePart());
    }

    public void CreateNlo(Nlo nlo)
    {

        if (_nlo2list.Count >= _nlo1list.Count)
        {
            _nlo1list.Add(nlo);

            if (_nlo2list.Count > 0)
            {
                int n = Random.Range(0, _nlo2list.Count);
                nlo.SetMainTarget(_nlo2list[n]);
            }
            CreatePresenter(_nlo1Template, nlo);
        }
        else
        {
            _nlo2list.Add(nlo);

            if (_nlo1list.Count > 0)
            {
                int n = Random.Range(0, _nlo1list.Count);
                nlo.SetMainTarget(_nlo1list[n]);
            }
            CreatePresenter(_nlo2Template, nlo);
        }
        
    }

    public void CreateAsteroid(Asteroid asteroid)
    {
        AsteroidPresenter presenter = CreatePresenter(_asteroidTemplate, asteroid) as AsteroidPresenter;
        presenter.Init(this);
    }

    private Presenter CreatePresenter(Presenter template, Transformable model)
    {
        Presenter presenter = Instantiate(template);
        presenter.Init(model, _camera);

        return presenter;
    }
}
