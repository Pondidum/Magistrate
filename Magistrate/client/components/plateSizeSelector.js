import React from 'react'
import { connect } from 'react-redux'
import { setTileSize } from '../actions/'
import PlateSizes from './Plate/sizes'

const mapStateToProps = (state, ownProps) => {
  return {
    tileSize: state.ui.tileSize,
    ...ownProps
  }
}

const mapDispatchToProps = (dispatch) => {
    return {
      setTileSize: (size) => {
        dispatch(setTileSize(size));
        reactCookie.save('tileSize', size);
      }
    }
}

const PlateSizeSelector = ({ className, tileSize, setTileSize }) => (
  <li className={className}>
    <a
      href="#"
      className={tileSize == PlateSizes.small ? "active" : ""}
      onClick={e => setTileSize(PlateSizes.small)}>
        <span className="glyphicon glyphicon-th" />
    </a>
    <a
      href="#"
      className={tileSize == PlateSizes.large ? "active" : ""}
      onClick={e => setTileSize(PlateSizes.large)}>
        <span className="glyphicon glyphicon-th-large" />
    </a>
    <a
      href="#"
      className={tileSize == PlateSizes.table ? "active" : ""}
      onClick={e => setTileSize(PlateSizes.table)}>
        <span className="glyphicon glyphicon-th-list" />
    </a>
  </li>
)

export default connect(mapStateToProps, mapDispatchToProps)(PlateSizeSelector)
