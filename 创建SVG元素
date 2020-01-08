// 注意：直接用js或者jquery 直接创建的svg元素是不起效果的，必须用createElementNS
private createSVGElement(element, attrs) {
    var returnEle = document.createElementNS('http://www.w3.org/2000/svg', element);
    for (const key in attrs) {
        returnEle.setAttribute(key, attrs[key])
    }
    return returnEle
}
