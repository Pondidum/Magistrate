import React from 'react'

const PlateSmall = ({ title, onCross }) => (
  <li className="col-md-3 tile">
    <div className="panel panel-default">
      <PlateHeader title={title} onCross={onCross} />
    </div>
  </li>
)

export default PlateSmall
