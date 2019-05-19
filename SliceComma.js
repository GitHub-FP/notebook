function SliceComma(Num){
    var arrUser=Num.toString().split(".");
    var number=arrUser[0];
    var value=number.toString();
    var ToStrings=value.split("").reverse().join('');
    var length = value.split("").length;
    var len=null;
    var remainder=null;
    if(length%3 > 0){
        remainder = length%3;
        len=Math.floor(length/3);
    }
    else{
        len=Math.floor(length/3)-1;
    }
    if(len>0){
        value= ToStrings.slice(0,3);
        var start=3;
        var end=6;
        for(var i=1;i<=len;i++){
            value=value+","+ToStrings.slice(start,end);
            start+=3;end+=3;
        }
    }
    if(arrUser.length==2){
        value = value.toString().split('').reverse().join('')+'.'+arrUser[1];
    }
    else{
        value = value.toString().split('').reverse().join('');
    }
    return value;
}
