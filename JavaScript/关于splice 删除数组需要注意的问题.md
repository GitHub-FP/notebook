关于js 中的使用splice去批量删除数组需要注意的地方：1.删除下标数组建议由大到小排序
                                                  2.建议从循环从数组的的最后一个下标开始，以0下标结束。
                                                  
                          
      function spliceArr(indexArr, returnValue, name) {                        //解决 splice 错位的问题
            indexArr = indexArr.sort(function (a, b) {                          //由大到小排序
                return b - a;
            });
            indexArr.forEach(element => {
                // let length = returnValue[name].length - 1;
                // for (let i = length; i >= 0; i--) {
                returnValue[name].splice(element, 1);
                // }
            });
        }
