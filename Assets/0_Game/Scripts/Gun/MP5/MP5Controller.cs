using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditorInternal.ReorderableList;

public class MP5Controller : BaseGunController
{

    private void Start()
    {
        DamageTalent.CurrentTalentLevel = 0;
        AmmoTalent.CurrentTalentLevel = 0;
        PierceTalent.CurrentTalentLevel = 0;

        magazineCapacity = AmmoTalent.GetMaxAmmoCount();
        UIController.Instance.SetCurrentAmmoText(magazineCapacity);

        bulletDamage = DamageTalent.GetDamageAmount();
    }

    public override void Fire()
    {
        if (Time.time >= nextFireTime && magazineCapacity > 0)
        {
            Ray ray = fpsCamera.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
            RaycastHit hit;

            Vector3 targetPoint;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~aimIgnoreLayerMask))
            {
                targetPoint = hit.point;
                print(hit.transform.name);
            }
            else
                targetPoint = ray.GetPoint(75f);

            Vector3 direction = targetPoint - firePoint.position;

            Bullet currentBullet = ObjectPooler.Instance.GetPooledObject(0).GetComponent<Bullet>();
            currentBullet.transform.position = firePoint.position;

            currentBullet.transform.forward = direction.normalized;
            currentBullet.gameObject.SetActive(true);
            currentBullet.Init(DamageTalent.GetDamageAmount(), bulletSpeed, PierceTalent.IsPierceOpened, direction.normalized);

            magazineCapacity--;
            UIController.Instance.SetCurrentAmmoText(magazineCapacity);
            nextFireTime = Time.time + fireDelay;
            animator.Play(StringHelper.FIRE, -1, 0);

            muzzleParticle.Play();
        }
    }


}
