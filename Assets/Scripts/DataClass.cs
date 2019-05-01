using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;

public class DataClass : ScriptableObject, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    //Default to Green
    //TODO: pull from local data
    private Color _MY_COLOR = new Color(105, 224, 99, 255);
    public Color TheColor {
        get { return _MY_COLOR; }
        set {
            if(_MY_COLOR == value) {
                return;
            }
            _MY_COLOR = value;
            OnPropertyChanged("TheColor");
        }
    }

    public Color MyGetColor(string s)
    {
        if (s == "Red")
        {
            return new Color(226, 97, 97, 255);
        }
        else if (s == "Orange")
        {
            return new Color(237, 151, 81, 255);
        }
        else if (s == "Yellow")
        {
            return new Color(229, 207, 83, 255);
        }
        else if (s == "Teal")
        {
            return new Color(99, 224, 220, 255);
        }
        else if (s == "Purple")
        {
            return new Color(172, 99, 224, 255);
        }
        else if (s == "Pink")
        {
            return new Color(239, 67, 202, 255);
        }
        else
        {   
            // This is Green
            return new Color(105, 224, 99, 255);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //TODO: pull from local data
        _MY_COLOR = new Color(105, 224, 99, 255);
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/

    private void OnPropertyChanged(string propertyName) {
        if(PropertyChanged != null) {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
