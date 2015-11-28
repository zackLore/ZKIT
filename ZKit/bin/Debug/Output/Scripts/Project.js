var Person = function() {

this.Name = new Obj('txt_Person_Name');
this.Name.valType = 'str';
this.Name.setDefaultVal('Name Here');
this.Name.setDefaultColor('#8888FF');

this.Age = new Obj('txt_Person_Age');
this.Age.valType = 'int';
this.Age.setDefaultVal('0');
this.Age.setDefaultColor('#8888FF');

this.Height = new Obj('txt_Person_Height');
this.Height.valType = 'dbl';
this.Height.setDefaultVal('0');
this.Height.setDefaultColor('#8888FF');

this.collectValues = function() {
var returnObject = {
Name : this.Name.val ,
Age : this.Age.val ,
Height : this.Height.val
};

return returnObject;

};

}

