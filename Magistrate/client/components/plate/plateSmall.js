import React from 'react'
import PlateHeader from './PlateHeader'

const PlateSmall = ({ title, onCross }) => (
  <li className="col-md-3 tile">
    <div className="panel panel-default">
      <PlateHeader title={title} onCross={onCross} />
    </div>
  </li>
)

export default PlateSmall
