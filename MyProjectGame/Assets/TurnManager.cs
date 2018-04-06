using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    static Dictionary<string, List<TacticsMovement>> units = new Dictionary<string, List<TacticsMovement>>();
    static Queue<string> turnKey = new Queue<string>();
    static Queue<TacticsMovement> turnTeam = new Queue<TacticsMovement>();
    static List<TacticsMovement> list;

    void Start ()
    {
		
	}
	
	void Update ()
    {
        if (turnTeam.Count == 0)
        {
            InitTeamTurnQueue();
        }
	}

    static void InitTeamTurnQueue()
    {
        List<TacticsMovement> teamList = units[turnKey.Peek()];

        foreach (TacticsMovement unit in teamList)
        {
            turnTeam.Enqueue(unit);
        }

        StartTurn();
    }

    public static void StartTurn()
    {
        if (turnTeam.Count > 0)
        {
            turnTeam.Peek().BeginTurn();
        }
    }

    public static void EndTurn()
    {
        TacticsMovement unit = turnTeam.Dequeue();
        unit.EndTurnUnit();

        if (turnTeam.Count > 0)
        {
            StartTurn();
        }
        else
        {
            string team = turnKey.Dequeue();
            turnKey.Enqueue(team);
            InitTeamTurnQueue();
        }
    }

    public void ButtonEndTurn()
    {
        EndTurn();
    }

    public static void AddUnit(TacticsMovement unit)
    {
        if (!units.ContainsKey(unit.tag))
        {
            list = new List<TacticsMovement>();
            units[unit.tag] = list;

            if (!turnKey.Contains(unit.tag))
            {
                turnKey.Enqueue(unit.tag);
            }
        }
        else
        {
            list = units[unit.tag];
        }

        list.Add(unit);
    }

    public static void DeleteUnit(TacticsMovement unit)
    {
        turnTeam.Enqueue(unit);
        list.Remove(unit);
        units[unit.tag].Remove(unit);
        Destroy(unit.gameObject);
    }
}
