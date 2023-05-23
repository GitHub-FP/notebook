import { useState, useRef, useEffect } from 'react';


// let isMouseDown = false;
function Move(props) {
  const [position, setPosition] = useState({ x: "95%", y: "90%" });
  const iconRef = useRef(null);
  const screenRef = useRef({ screenX:0,screenY:0 });
  const positionRef = useRef({ x: 0, y: 0 });

  // 处理DOM经过iframe的同域时位置不准确的问题
  function computePosition(e){
    const { x, y,screenX,screenY } = e;

    let offsetScreenX= screenX - screenRef.current.screenX
    let offsetX= x - positionRef.current.x
    let realX = x;
    if(offsetScreenX!=offsetX){
      realX += offsetScreenX - offsetX;
    }

    //
    let realY = y;
    let offsetScreenY = screenY - screenRef.current.screenY;
    let offsetY= y - positionRef.current.y;
    if(offsetScreenY!=offsetY){
      realY += offsetScreenY - offsetY;
    }

    return {
      x:realX,
      y:realY
    }
  }

  // 处理拖动Dom到边界
  function processBoundary(px,py){
    
    const { left, top, width, height } = iconRef.current.getBoundingClientRect();
    let docWidth = document.body.clientWidth;
    let docHeiight = document.body.clientHeight;
    if (px < 0) {
      px = 0
    }
    if ((px + width) > docWidth) {
      px = docWidth - width
    }

    if (py < 0) {
      py = 0
    }
    if ((py + height) > docHeiight) {
      py = docHeiight - height
    }

    return {
      x:px,
      y:py
    }
  }

  useEffect(function () {

    if (iconRef.current) {
      iconRef.current.addEventListener('dragstart', function (event) {

        const {x, y,screenX,screenY } = event;
        screenRef.current = { screenX,screenY };
        positionRef.current = { x , y };
        // 设置传输的数据和拖动效果
        event.dataTransfer.setData('text/plain', 'This text may be dragged');
        event.dataTransfer.dropEffect = 'move';
      });

      iconRef.current.addEventListener('dragend', function (e) {
        // 设置传输的数据和拖动效果
        
        const cp =  computePosition(e);
        let px = cp.x;
        let py = cp.y;

        const pb = processBoundary(px,py)

        setPosition({
          x: pb.x + "px",
          y: pb.y + "px",
        });
      });
    }
  }, []);

  return (
    <div
      ref={iconRef}
      draggable="true"
      style={{ zIndex: 10000, position: 'fixed', left: `${position.x}`, top: `${position.y}`, color: 'red' }}
      onClick={(e) => {
        props.clickEvent();
      }}
    >
      <div>
        {props.children}
      </div>
    </div>
  );
}

export default Move;