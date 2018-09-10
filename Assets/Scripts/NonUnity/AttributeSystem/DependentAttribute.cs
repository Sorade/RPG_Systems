using System;


[Serializable]
public class DependentAttribute : Attribute {
    public Attribute[] _otherAttributes;


    /*public function addAttribute(attr:Attribute):void
        {
            _otherAttributes.push(attr);
        }

    public function removeAttribute(attr:Attribute):void
            {
                if (_otherAttributes.indexOf(attr) >= 0)
                {
                    _otherAttributes.splice(_otherAttributes.indexOf(attr), 1);
                }
            }*/    
}
