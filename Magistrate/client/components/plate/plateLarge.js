import React from 'react'
import PlateChildren from './PlateChildren'

const PlateLarge = ({ title, onCross, tileSize, children }) => (
  <li className="col-md-3 tile">
    <div className="panel panel-default">
      <PlateHeader title={title} onCross={onCross} />
      <div className="panel-body" style={{ height: "100px" }}>
        <PlateChildren tileSize={tileSize} children={children} />
      </div>
    </div>
  </li>
);

export default PlateLarge
