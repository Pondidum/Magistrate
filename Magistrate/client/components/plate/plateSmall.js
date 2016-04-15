import React from 'react'
import PlateHeader from './PlateHeader'

const PlateSmall = ({ title, onClick, onCross, additions }) => (
  <li className="col-md-3 tile">
    <div className="panel panel-default">
      <PlateHeader title={title} onClick={onClick} onCross={onCross} />
    </div>
    {additions}
  </li>
)

export default PlateSmall
