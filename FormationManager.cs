using System.Collections.Generic;
using UnityEngine;

public class FormationManager : MonoBehaviour
{
    public static FormationManager instance { get; private set; }

    private Dictionary<Enums.Team, Vector3[]> teamFormations = new();

    [SerializeField] private float spacingZ = 8f;
    [SerializeField] private float spacingX = 10f;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    public void InitializeFormations()
    {
        teamFormations[Enums.Team.TeamA] = GenerateFormation(Enums.Team.TeamA);
        teamFormations[Enums.Team.TeamB] = GenerateFormation(Enums.Team.TeamB);
    }

    private Vector3[] GenerateFormation(Enums.Team team)
    {
        Vector3[] positions = new Vector3[11];

        float baseZ = team == Enums.Team.TeamA ? TeamManager.instance.TeamAGoal.transform.position.z + spacingZ : TeamManager.instance.TeamBGoal.transform.position.z - spacingZ;

        // Example 4-3-3 layout
        positions[0] = new Vector3(0, 0, baseZ); // GK
        positions[1] = new Vector3(-spacingX * 1.5f, 0, baseZ + spacingZ); // LB
        positions[2] = new Vector3(-spacingX * 0.5f, 0, baseZ + spacingZ); // CB
        positions[3] = new Vector3(spacingX * 0.5f, 0, baseZ + spacingZ);  // CB
        positions[4] = new Vector3(spacingX * 1.5f, 0, baseZ + spacingZ);  // RB
        positions[5] = new Vector3(-spacingX, 0, baseZ + spacingZ * 2);    // CM
        positions[6] = new Vector3(0, 0, baseZ + spacingZ * 2);            // CM
        positions[7] = new Vector3(spacingX, 0, baseZ + spacingZ * 2);     // CM
        positions[8] = new Vector3(-spacingX, 0, baseZ + spacingZ * 3);    // LW
        positions[9] = new Vector3(0, 0, baseZ + spacingZ * 3);            // ST
        positions[10] = new Vector3(spacingX, 0, baseZ + spacingZ * 3);    // RW

        return positions;
    }

    public Vector3 GetFormationPosition(Enums.Team team, int index)
    {
        if (teamFormations.TryGetValue(team, out var formation))
        {
            if (index >= 0 && index < formation.Length)
                return formation[index];
        }
        return Vector3.zero;
    }
}