import React from 'react'
import { connect } from 'react-redux'

import PlateSizes from './sizes'
import PlateSmall from './PlateSmall'
import PlateLarge from './PlateLarge'
import PlateRow from './PlateRow'

const mapStateToProps = (state, ownProps) => {
  return {
    tileSize: state.ui.tileSize,
    ...ownProps
  }
}

const Plate = (props) => {

  const tileSize = props.tileSize;

  if (tileSize == PlateSizes.large)
    return <PlateLarge {...props} />

  if (tileSize == PlateSizes.table)
    return <PlateRow {...props} />

  return <PlateSmall {...props} />
}

export default connect(mapStateToProps)(Plate)
