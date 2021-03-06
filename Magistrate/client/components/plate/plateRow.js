import React from 'react'
import PlateChildren from './PlateChildren'

const PlateRow = ({ title, onClick, onCross, tileSize, additions, children }) => (
  <li className="col-md-12 tile">
    <div className="row panel panel-default">
      <div className="panel-body">
        <div className="col-sm-3"><a href="#" onClick={onClick}>{title}</a></div>
        <div className="col-sm-8"><PlateChildren tileSize={tileSize} children={children} /></div>
        <div className="col-sm-1">
          <a href="#" onClick={onCross}>
            <span className="glyphicon glyphicon-remove-circle"></span>
          </a>
        </div>
      </div>
    </div>
    {additions}
  </li>
)

export default PlateRow
