using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpriteDatabase", menuName = "ScriptableObjects/EnemySpriteDatabase")]
public class EnemySpriteDatabase : ScriptableObject
{
	
	[System.Serializable]
    public class EnemyData
    {
        public string enemyName;
        public Sprite sprite;
        public Vector3 spriteSize;
        public RuntimeAnimatorController animatorController; // Add animator controller reference
    }
	
	public EnemyData[] enemyDataArray;

    public EnemyData GetEnemyDataByName(string enemyName)
    {
        foreach (var enemyData in enemyDataArray)
        {
            if (enemyData.enemyName == enemyName)
            {
				Debug.Log("Sprite "+enemyName+" for "+enemyData.enemyName+" found");
                return enemyData;
            }
        }
		Debug.Log("Sprite "+enemyName+" not found");
        return null; // Return null if not found
    }
}
