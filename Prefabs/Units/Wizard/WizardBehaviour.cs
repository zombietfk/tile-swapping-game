﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardBehaviour : JumpingUnit
{
    [SerializeField]
    private SpriteRenderer Renderer;
    [SerializeField]
    private Animator Animation;
    [SerializeField]
    private WizardSpell Fireball;
    private float FireTimer = 8f;
    private float cFireTimer = 0f;

    public override void Update() {
        base.Update();
        cFireTimer += Time.deltaTime;
        if(cFireTimer > FireTimer) {
            cFireTimer = 0f;
            ShootFireball();
        }
    }

    public void ShootFireball() {
        WizardSpell Projectile = Instantiate(Fireball);
        Projectile.transform.localPosition = new Vector3(
            transform.localPosition.x,
            transform.localPosition.y,
            5f
        );
        Projectile.transform.SetParent(transform.parent, true);
        Projectile.SetOwner(gameObject);
        var settings = Projectile.GetComponentInChildren<ParticleSystem>().main;
        var gradient = new Gradient();
        gradient.SetKeys(new GradientColorKey[] { new GradientColorKey(
            Renderer.color,
            0
        ), new GradientColorKey(
            Renderer.color,
            1
        ) }, new GradientAlphaKey[] {
            new GradientAlphaKey(255, 1),
            new GradientAlphaKey(255, 0.9f),
            new GradientAlphaKey(0, 0),
        });
        settings.startColor = new ParticleSystem.MinMaxGradient(
            gradient
        );
        var trail = Projectile.GetComponentInChildren<TrailRenderer>();
        trail.startColor = Renderer.color;
        trail.endColor = new Color(
            Renderer.color.r,
            Renderer.color.g,
            Renderer.color.b,
            0
        );
        Projectile.Shoot(
            new Vector3(
                Body.velocity.x > 0 ? 1f : -1f,
                0f,
                0f
            ),
            8f
        );
    }
}
