using UnityEngine;
using System.IO;
namespace Defend.Manager
{
    /// <summary>
    /// 세이브 로드 기능 
    /// </summary>
    public class DataManager : MonoBehaviour
    {
        static GameObject container;

        //싱글톤
        static DataManager instance;
        public static DataManager Instance
        {

            get
            {
                if (!instance)
                {
                    container = new GameObject();
                    container.name = "DataManager";
                    instance = container.AddComponent(typeof(DataManager)) as DataManager;
                    DontDestroyOnLoad(container);
                }

                return instance;
            }
        }

        //게임 데이터 파일이름 .json
        string GameDataFileName = "GameData.json";



        //불러오기
        public void LoadGameData()
        {
            Data data = null;

            // 경로 지정 //Application.dataPath 프로젝트/에셋 으로 지정
            // 윈도우만 됨
            string dataPath = Application.dataPath + "/" + GameDataFileName;
            // 저장된 게임이 있다면

            if (File.Exists(dataPath))
            {

                // 저장된 파일 읽어오고 Json을 클래스 형식으로 전환해서 할당
                string FromJsonData = File.ReadAllText(dataPath);
                data = JsonUtility.FromJson<Data>(FromJsonData);
                Debug.Log(data.money);
                print("불러오기 완료");
            }
            else
            {
                data = new Data(); // 기본 데이터 생성
            }

        }

        //저장하기

        public void SaveGameData(Data data)
        {
            // 클래스를 Json 형식으로 전환 (true : 가독성 좋게 작성)
            //기본이 false 라 들여쓰기 띄어쓰기 안돼있음
            string ToJsonData = JsonUtility.ToJson(data, true);
            //경로찾기
            string dataPath = Application.dataPath + "/" + GameDataFileName;

            // 이미 저장된 파일이 있다면 덮어쓰고, 없다면 새로 만들어서 저장
            File.WriteAllText(dataPath, ToJsonData);
            Debug.Log(data.money);
            // 올바르게 저장됐는지 확인용
            Debug.Log("저장 완료");

        }


        public void DeleteGameData(Data data)
        {
            data = new Data();
            // 클래스를 Json 형식으로 전환 (true : 가독성 좋게 작성)
            //기본이 false 라 들여쓰기 띄어쓰기 안돼있음
            string ToJsonData = JsonUtility.ToJson(data, true);
            //경로찾기
            string dataPath = Application.dataPath + "/" + GameDataFileName;

            // 이미 저장된 파일이 있다면 덮어쓰고, 없다면 새로 만들어서 저장
            File.WriteAllText(dataPath, ToJsonData);
        
            // 올바르게 저장됐는지 확인용
          

        }
    }
}
