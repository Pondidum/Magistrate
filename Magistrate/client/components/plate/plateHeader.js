import React from 'react'

const PlateHeader = ({ title, onClick, onCross }) => (
  <div className="panel-heading" onClick={onClick}>
    <h3 className="panel-title">
      {title}
      <ul className="tile-actions pull-right list-unstyled list-inline">
        <li>
          <a href="#" onClick={onCross}>
            <span className="glyphicon glyphicon-remove-circle"></span>
          </a>
        </li>
      </ul>
    </h3>
  </div>
);

export default PlateHeader
