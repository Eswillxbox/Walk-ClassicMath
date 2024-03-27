using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Layout : MonoBehaviour
{
    // Start is called before the first frame update
    public InputField _user;
    public InputField _password;
    public Text _error;
    public Button _load;
    public Button _sign;
    void Start()
    {
        if (_error.IsActive())
            _error.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Loading()
    {
        FileStream fs = new FileStream("C:\\Users\\Administrator\\Documents\\user.txt", FileMode.OpenOrCreate);
        if (!check(fs, _user.text, _password.text))
            _error.gameObject.SetActive(true);
        else
            SceneManager.LoadScene("Walk_Simulator");
    }

    public void Signing()
    {
        FileStream fs = new FileStream("C:\\Users\\Administrator\\Documents\\user.txt", FileMode.OpenOrCreate);
        if (!check(fs, _user.text, _password.text) && !_user && !_password)
        {
            StreamWriter writer = new StreamWriter(fs);
            writer.WriteLine(_user.text);
            writer.WriteLine(_password.text);
            writer.Close();
            _error.gameObject.SetActive(false);
        }
        else
            _error.gameObject.SetActive(true);
    }

    private bool check(FileStream fileStream, String userName, string password)
    {
        StreamReader reader = new StreamReader(fileStream);
        int i = 0;
        while (true)
        {
            String res = reader.ReadLine();
            if (res == null)
                break;
            if (res.Equals(userName) && i % 2 == 0)
            {
                i++;
                if (password.Equals(reader.ReadLine()))
                {
                    return true;
                    reader.Close();
                }
                else
                    return false;
            }
            i++;
        }
        return false;
    }
}
