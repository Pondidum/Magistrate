import React from 'react'
import { connect } from 'react-redux'

import Tile from '../tile'

const mapStateToProps = (state, ownProps) => {
  return {
    tileSize: state.ui.tileSize,
    ...ownProps
  }
}

const Plate = (props) => {

  const tileSize = props.tileSize;

  if (tileSize == Tile.sizes.large)
    return <PlateLarge {...props} />

  if (tileSize == Tile.sizes.table)
    return <PlateRow {...props} />

  return <PlateSmall {...props} />
}

export default connect(mapStateToProps)(Plate)
