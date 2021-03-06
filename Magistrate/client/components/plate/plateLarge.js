import React from 'react'
import PlateHeader from './PlateHeader'
import PlateChildren from './PlateChildren'

const PlateLarge = ({ title, onClick, onCross, tileSize, additions, children }) => (
  <li className="col-md-3 tile">
    <div className="panel panel-default">
      <PlateHeader title={title} onClick={onClick} onCross={onCross} />
      <div className="panel-body" style={{ height: "100px" }}>
        <PlateChildren tileSize={tileSize} children={children} />
      </div>
    </div>
    {additions}
  </li>
);

export default PlateLarge
