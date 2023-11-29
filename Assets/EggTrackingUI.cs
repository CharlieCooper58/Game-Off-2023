using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EggTrackingUI : MonoBehaviour
{
    [SerializeField] EggTrackerUI EggTracker;
    [SerializeField] List<Spawner> Spawns;
    [SerializeField] List<EggTrackerUI> Trackers;
    // Start is called before the first frame update
    void Start()
    {
    }
    void CreateTrackers() {
        Spawns = FindObjectsByType<Spawner>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();//All spawners in the scene, including the inactive ones.
        foreach (var tracker in Trackers) {
            Destroy(tracker);
        }
        foreach (var spawner in Spawns) {
            var tracker = Instantiate(EggTracker, transform);
            tracker.AssignSpawner(spawner);
            Trackers.Add(tracker);

        }
    }
    void RefreshTrackers() {
        foreach (var tracker in Trackers) {
            tracker.gameObject.SetActive(tracker.spawner!=null &&tracker.spawner.isActiveAndEnabled);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Spawns.Count == 0)
            CreateTrackers();
        RefreshTrackers();
    }
}
