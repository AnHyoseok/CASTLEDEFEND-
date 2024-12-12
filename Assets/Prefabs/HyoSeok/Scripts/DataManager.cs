using UnityEngine;
using System.IO;
namespace Defend.Manager
{
    /// <summary>
    /// ���̺� �ε� ��� 
    /// </summary>
    public class DataManager : MonoBehaviour
    {
        static GameObject container;

        //�̱���
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

        //���� ������ �����̸� .json
        string GameDataFileName = "GameData.json";

        // ����� Ŭ���� ����
        public Data data = new Data();

        //�ҷ�����
        public void LoadGameData()
        {
            // ��� ���� //Application.dataPath ������Ʈ/���� ���� ����
            // �����츸 ��
            string dataPath = Application.dataPath + "/Json/" + GameDataFileName;
            // ����� ������ �ִٸ�
            if (File.Exists(dataPath))
            {
                // ����� ���� �о���� Json�� Ŭ���� �������� ��ȯ�ؼ� �Ҵ�
                string FromJsonData = File.ReadAllText(dataPath);
                data = JsonUtility.FromJson<Data>(FromJsonData);
                print("�ҷ����� �Ϸ�");
            }
            else
            {
                data = new Data(); // �⺻ ������ ����
            }

        }

        //�����ϱ�

        public void SaveGameData()
        {
            // Ŭ������ Json �������� ��ȯ (true : ������ ���� �ۼ�)
            //�⺻�� false �� �鿩���� ���� �ȵ�����
            string ToJsonData = JsonUtility.ToJson(data, true);
            //���ã��
            string dataPath = Application.dataPath + "/Json/" + GameDataFileName;

            // �̹� ����� ������ �ִٸ� �����, ���ٸ� ���� ���� ����
            File.WriteAllText(dataPath, ToJsonData);

            // �ùٸ��� ����ƴ��� Ȯ�ο�
            print("���� �Ϸ�");
            for (int i = 0; i < data.isClear.Length; i++)
            {
                print($"{i}�� é�� ��� ���� ���� : " + data.isClear[i]);
            }
        }



    }
}
