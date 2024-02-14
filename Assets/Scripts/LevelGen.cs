using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGen : MonoBehaviour
{

    public GameObject[] Enemys;
    public GameObject LastGen;
    public GameObject[] CurEnemys;
    private float LastEnemyNum;

    public float DisX;
    public float DisY;

    public float LastDisFromPlayer;

    public float LastPosX;
    public float LastPosY;

    public float MaxPosY;
    public float MinPosY;

    public int MaxEnemysPerRow;
    public int MinEnemysPerRow;

    private int LastRow = 0;
    private float LastTry = 3;

    // Use this for initialization
    private void Awake ()
    {

        LastDisFromPlayer = 0f;

	}

    // Update is called once per frame
    private void Update()
    {

        CurEnemys = GameObject.FindGameObjectsWithTag("Bad");

        while (LastDisFromPlayer < 15.99f)
        {
            MakeNewRow();
        }
        //Once the dis is too far away.
        if (LastGen != null)
        {
            LastDisFromPlayer = Vector2.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, LastGen.transform.position);
        }
        //If the code errors and stopps making enemys.
        if (LastGen == null)
        {
            if (CurEnemys.Length > 0)
            {
                GameObject NewLastGen = null;
                float NewLastPosX = 0;
                for (int i = 0; CurEnemys.Length > i; i++)
                {
                    if(CurEnemys[i].transform.position.x > NewLastPosX)
                    {
                        NewLastPosX = CurEnemys[i].transform.position.x;
                        NewLastGen = CurEnemys[i];
                    }
                }
                LastGen = NewLastGen;
                LastPosX = NewLastPosX + DisX;
            }
            else
            {
                NoEnemysFix();
            }
        }

	}

    private void MakeNewRow()
    {

        int RandomNum = Random.Range(1, 5);
        //Makes sure the level is fair and not buggy.
        #region
        //If this is not the first row.
        if (LastRow != 0)
        {
            //If we have a top row and a bottom row next to each other.
            if (RandomNum == 1 && LastRow == 3 || RandomNum == 3 && LastRow == 1)
            {
                RandomNum = 2;
            }
            //If we have to much blank space
            if(RandomNum == 4 && LastRow == 4)
            {
                RandomNum = Random.Range(1, 4);
            }
        }
        #endregion
        LastPosX += DisX;

        if(RandomNum == 1)
        {
            StartFromTop();
        }
        if(RandomNum == 3)
        {
            StartFromBottom();
        }
        if(RandomNum == 2)
        {
            RandomStart();
        }

    }

    private void StartFromTop()
    {

        //Chosing a random enemy to spawn.
        #region
        float RanNum = Random.Range(0, 21);
        GameObject ToGen = null;
        if (RanNum <= 10f)
        {
            ToGen = Enemys[0];
            LastEnemyNum = 0;
        }
        else if (RanNum > 10f && RanNum <= 15f)
        {
            ToGen = Enemys[1];
            LastEnemyNum = 1;
        }
        else if (RanNum > 15f && RanNum <= 18f)
        {
            ToGen = Enemys[2];
            if(LastEnemyNum == 2)
            {
                ToGen = Enemys[0];
            }else
            {
                LastEnemyNum = 2;
            }
        }
        else if (RanNum > 18f)
        {
            ToGen = Enemys[3];
            if (LastEnemyNum == 3)
            {
                ToGen = Enemys[0];
            }
            else
            {
                LastEnemyNum = 3;
            }
        }
        #endregion
        //Picks a random number of enemys to gen
        int HowManyGen = Random.Range(MinEnemysPerRow,MaxEnemysPerRow);
        bool FirstTime = true;
        
        //Loops and makes new enemys
        while(HowManyGen > 0)
        {

            if (FirstTime)
            {
                LastGen = Instantiate(ToGen, new Vector3(LastPosX, MaxPosY), transform.rotation);
                LastPosY = MaxPosY;
                LastDisFromPlayer = Vector2.Distance
                    (
                    GameObject.FindGameObjectWithTag("Player").transform.position,
                    LastGen.transform.position
                    );
                HowManyGen--;
                FirstTime = false;
                #region
                RanNum = Random.Range(0, 21);
                ToGen = null;
                if (RanNum <= 10f)
                {
                    ToGen = Enemys[0];
                    LastEnemyNum = 0;
                }
                else if (RanNum > 10f && RanNum <= 15f)
                {
                    ToGen = Enemys[1];
                    LastEnemyNum = 1;
                }
                else if (RanNum > 15f && RanNum <= 18f)
                {
                    ToGen = Enemys[2];
                    if (LastEnemyNum == 2)
                    {
                        ToGen = Enemys[0];
                    }
                    else
                    {
                        LastEnemyNum = 2;
                    }
                }
                else if (RanNum > 18f)
                {
                    ToGen = Enemys[3];
                    if (LastEnemyNum == 3)
                    {
                        ToGen = Enemys[0];
                    }
                    else
                    {
                        LastEnemyNum = 3;
                    }
                }
                #endregion
            }
            else
            {
                LastPosY -= DisY;
                LastGen = Instantiate(ToGen, new Vector3(LastPosX, LastPosY), transform.rotation);
                LastDisFromPlayer = Vector2.Distance
                    (
                    GameObject.FindGameObjectWithTag("Player").transform.position,
                    LastGen.transform.position
                    );
                HowManyGen--;
                #region
                RanNum = Random.Range(0, 21);
                ToGen = null;
                if (RanNum <= 10f)
                {
                    ToGen = Enemys[0];
                    LastEnemyNum = 0;
                }
                else if (RanNum > 10f && RanNum <= 15f)
                {
                    ToGen = Enemys[1];
                    LastEnemyNum = 1;
                }
                else if (RanNum > 15f && RanNum <= 18f)
                {
                    ToGen = Enemys[2];
                    if (LastEnemyNum == 2)
                    {
                        ToGen = Enemys[0];
                    }
                    else
                    {
                        LastEnemyNum = 2;
                    }
                }
                else if (RanNum > 18f)
                {
                    ToGen = Enemys[3];
                    if (LastEnemyNum == 3)
                    {
                        ToGen = Enemys[0];
                    }
                    else
                    {
                        LastEnemyNum = 3;
                    }
                }
                #endregion
            }


        }

    }

    private void StartFromBottom()
    {

        //Chosing a random enemy to spawn.
        #region
        float RanNum = Random.Range(0, 21);
        GameObject ToGen = null;
        if (RanNum <= 10f)
        {
            ToGen = Enemys[0];
            LastEnemyNum = 0;
        }
        else if (RanNum > 10f && RanNum <= 15f)
        {
            ToGen = Enemys[1];
            LastEnemyNum = 1;
        }
        else if (RanNum > 15f && RanNum <= 18f)
        {
            ToGen = Enemys[2];
            if (LastEnemyNum == 2)
            {
                ToGen = Enemys[0];
            }
            else
            {
                LastEnemyNum = 2;
            }
        }
        else if (RanNum > 18f)
        {
            ToGen = Enemys[3];
            if (LastEnemyNum == 3)
            {
                ToGen = Enemys[0];
            }
            else
            {
                LastEnemyNum = 3;
            }
        }
        #endregion
        //Picks a random number of enemys to gen
        int HowManyGen = Random.Range(MinEnemysPerRow, MaxEnemysPerRow);
        bool FirstTime = true;

        //Loops and makes new enemys
        while (HowManyGen > 0)
        {

            if (FirstTime)
            {
                LastGen = Instantiate(ToGen, new Vector3(LastPosX, MinPosY), transform.rotation);
                LastPosY = MinPosY;
                LastDisFromPlayer = Vector2.Distance
                    (
                    GameObject.FindGameObjectWithTag("Player").transform.position,
                    LastGen.transform.position
                    );
                HowManyGen--;
                FirstTime = false;
                #region
                RanNum = Random.Range(0, 21);
                ToGen = null;
                if (RanNum <= 10f)
                {
                    ToGen = Enemys[0];
                    LastEnemyNum = 0;
                }
                else if (RanNum > 10f && RanNum <= 15f)
                {
                    ToGen = Enemys[1];
                    LastEnemyNum = 1;
                }
                else if (RanNum > 15f && RanNum <= 18f)
                {
                    ToGen = Enemys[2];
                    if (LastEnemyNum == 2)
                    {
                        ToGen = Enemys[0];
                    }
                    else
                    {
                        LastEnemyNum = 2;
                    }
                }
                else if (RanNum > 18f)
                {
                    ToGen = Enemys[3];
                    if (LastEnemyNum == 3)
                    {
                        ToGen = Enemys[0];
                    }
                    else
                    {
                        LastEnemyNum = 3;
                    }
                }
                #endregion
            }
            else
            {
                LastPosY += DisY;
                LastGen = Instantiate(ToGen, new Vector3(LastPosX, LastPosY), transform.rotation);
                LastDisFromPlayer = Vector2.Distance
                    (
                    GameObject.FindGameObjectWithTag("Player").transform.position,
                    LastGen.transform.position
                    );
                HowManyGen--;
                #region
                RanNum = Random.Range(0, 21);
                ToGen = null;
                if (RanNum <= 10f)
                {
                    ToGen = Enemys[0];
                    LastEnemyNum = 0;
                }
                else if (RanNum > 10f && RanNum <= 15f)
                {
                    ToGen = Enemys[1];
                    LastEnemyNum = 1;
                }
                else if (RanNum > 15f && RanNum <= 18f)
                {
                    ToGen = Enemys[2];
                    if (LastEnemyNum == 2)
                    {
                        ToGen = Enemys[0];
                    }
                    else
                    {
                        LastEnemyNum = 2;
                    }
                }
                else if (RanNum > 18f)
                {
                    ToGen = Enemys[3];
                    if (LastEnemyNum == 3)
                    {
                        ToGen = Enemys[0];
                    }
                    else
                    {
                        LastEnemyNum = 3;
                    }
                }
                #endregion
            }


        }

    }

    private void RandomStart()
    {
        //Chosing a random enemy to spawn.
        #region
        float RanNum = Random.Range(0, 21);
        GameObject ToGen = null;
        if (RanNum <= 10f)
        {
            ToGen = Enemys[0];
            LastEnemyNum = 0;
        }
        else if (RanNum > 10f && RanNum <= 15f)
        {
            ToGen = Enemys[1];
            LastEnemyNum = 1;
        }
        else if (RanNum > 15f && RanNum <= 18f)
        {
            ToGen = Enemys[2];
            if (LastEnemyNum == 2)
            {
                ToGen = Enemys[0];
            }
            else
            {
                LastEnemyNum = 2;
            }
        }
        else if (RanNum > 18f)
        {
            ToGen = Enemys[3];
            if (LastEnemyNum == 3)
            {
                ToGen = Enemys[0];
            }
            else
            {
                LastEnemyNum = 3;
            }
        }
        #endregion
        //Picks a random number of enemys to gen
        int HowManyGen = Random.Range(MinEnemysPerRow, MaxEnemysPerRow);
        float RandPosY = Random.Range(MinPosY, MaxPosY);
        bool FirstTime = true;

        //Loops and makes new enemys
        while (HowManyGen > 0)
        {

            if (FirstTime)
            {
                LastGen = Instantiate(ToGen, new Vector3(LastPosX, RandPosY), transform.rotation);
                LastPosY = RandPosY;
                LastDisFromPlayer = Vector2.Distance
                    (
                    GameObject.FindGameObjectWithTag("Player").transform.position,
                    LastGen.transform.position
                    );
                HowManyGen--;
                FirstTime = false;
                #region
                RanNum = Random.Range(0, 21);
                ToGen = null;
                if (RanNum <= 10f)
                {
                    ToGen = Enemys[0];
                    LastEnemyNum = 0;
                }
                else if (RanNum > 10f && RanNum <= 15f)
                {
                    ToGen = Enemys[1];
                    LastEnemyNum = 1;
                }
                else if (RanNum > 15f && RanNum <= 18f)
                {
                    ToGen = Enemys[2];
                    if (LastEnemyNum == 2)
                    {
                        ToGen = Enemys[0];
                    }
                    else
                    {
                        LastEnemyNum = 2;
                    }
                }
                else if (RanNum > 18f)
                {
                    ToGen = Enemys[3];
                    if (LastEnemyNum == 3)
                    {
                        ToGen = Enemys[0];
                    }
                    else
                    {
                        LastEnemyNum = 3;
                    }
                }
                #endregion
            }
            else
            {
                if (LastTry == 1 || LastTry == 3)
                {
                    LastPosY += DisY;
                    LastGen = Instantiate(ToGen, new Vector3(LastPosX, LastPosY), transform.rotation);
                }

                if(LastGen.transform.position.y > MaxPosY || LastTry == 2 || LastTry == 3)
                {
                    Destroy(LastGen);
                    if (LastTry == 2 || LastTry == 3)
                    {
                        LastPosY -= DisY;
                        LastPosY -= DisY;
                        LastGen = Instantiate(ToGen, new Vector3(LastPosX, LastPosY), transform.rotation);
                    }
                    else
                    {
                        HowManyGen = 0;
                        break;
                    }

                    if(LastGen.transform.position.y < MinPosY)
                    {
                        Destroy(LastGen);
                        HowManyGen = 0;
                        break;
                    }
                    else
                    {
                        LastTry = 2;
                    }

                    LastDisFromPlayer = Vector2.Distance
                    (
                    GameObject.FindGameObjectWithTag("Player").transform.position,
                    LastGen.transform.position
                    );
                    HowManyGen--;
                }
                else
                {
                    LastTry = 1;
                }

                LastDisFromPlayer = Vector2.Distance
                    (
                    GameObject.FindGameObjectWithTag("Player").transform.position,
                    LastGen.transform.position
                    );
                HowManyGen--;
                #region
                RanNum = Random.Range(0, 21);
                ToGen = null;
                if (RanNum <= 10f)
                {
                    ToGen = Enemys[0];
                    LastEnemyNum = 0;
                }
                else if (RanNum > 10f && RanNum <= 15f)
                {
                    ToGen = Enemys[1];
                    LastEnemyNum = 1;
                }
                else if (RanNum > 15f && RanNum <= 18f)
                {
                    ToGen = Enemys[2];
                    if (LastEnemyNum == 2)
                    {
                        ToGen = Enemys[0];
                    }
                    else
                    {
                        LastEnemyNum = 2;
                    }
                }
                else if (RanNum > 18f)
                {
                    ToGen = Enemys[3];
                    if (LastEnemyNum == 3)
                    {
                        ToGen = Enemys[0];
                    }
                    else
                    {
                        LastEnemyNum = 3;
                    }
                }
                #endregion
            }


        }
        LastTry = 3;
    }

    private void NoEnemysFix()
    {
        //Chosing a random enemy to spawn.
        #region
        float RanNum = Random.Range(0, 21);
        GameObject ToGen = null;
        if (RanNum <= 10f)
        {
            ToGen = Enemys[0];
            LastEnemyNum = 0;
        }
        else if (RanNum > 10f && RanNum <= 15f)
        {
            ToGen = Enemys[1];
            LastEnemyNum = 1;
        }
        else if (RanNum > 15f && RanNum <= 18f)
        {
            ToGen = Enemys[2];
            if (LastEnemyNum == 2)
            {
                ToGen = Enemys[0];
            }
            else
            {
                LastEnemyNum = 2;
            }
        }
        else if (RanNum > 18f)
        {
            ToGen = Enemys[3];
            if (LastEnemyNum == 3)
            {
                ToGen = Enemys[0];
            }
            else
            {
                LastEnemyNum = 3;
            }
        }
        #endregion
        //Picks a random number of enemys to gen
        int HowManyGen = Random.Range(MinEnemysPerRow, MaxEnemysPerRow);
        float RandPosY = Random.Range(MinPosY, MaxPosY);

        //Starts a new loop of enemys
        while (HowManyGen > 0)
        {

            if (LastGen == null)
            {
                LastGen = null;
                LastGen = Instantiate(ToGen, new Vector3
                    (
                    GameObject.FindGameObjectWithTag("Player").transform.position.x + DisX * 1.5f
                    , RandPosY), transform.rotation);
                LastPosY = RandPosY;
                LastDisFromPlayer = Vector2.Distance
                    (
                    GameObject.FindGameObjectWithTag("Player").transform.position,
                    LastGen.transform.position
                    );
                HowManyGen--;
            }
            else
            {
                LastPosY += DisY;
                LastGen = Instantiate(ToGen, new Vector3(LastPosX, LastPosY), transform.rotation);
                if (LastGen.transform.position.y > MaxPosY)
                {
                    Destroy(LastGen);
                    LastPosY -= DisY;
                    LastPosY -= DisY;
                    LastGen = Instantiate(ToGen, new Vector3(LastPosX, LastPosY), transform.rotation);

                    if (LastGen.transform.position.y < MinPosY)
                    {
                        HowManyGen = 0;
                        break;
                    }

                    LastDisFromPlayer = Vector2.Distance
                    (
                    GameObject.FindGameObjectWithTag("Player").transform.position,
                    LastGen.transform.position
                    );
                    HowManyGen--;
                }

                LastDisFromPlayer = Vector2.Distance
                    (
                    GameObject.FindGameObjectWithTag("Player").transform.position,
                    LastGen.transform.position
                    );
                HowManyGen--;
            }


        }

        MakeNewRow();

    }

}
