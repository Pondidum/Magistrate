import React from 'react'
import { connect } from 'react-redux'
import { setTileSize } from '../actions/'
import Tile from './tile'

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

const TileSizeSelector = ({ className, tileSize, setTileSize }) => (
  <li className={className}>
    <a
      href="#"
      className={tileSize == Tile.sizes.small ? "active" : ""}
      onClick={e => setTileSize(Tile.sizes.small)}>
        <span className="glyphicon glyphicon-th" />
    </a>
    <a
      href="#"
      className={tileSize == Tile.sizes.large ? "active" : ""}
      onClick={e => setTileSize(Tile.sizes.large)}>
        <span className="glyphicon glyphicon-th-large" />
    </a>
    <a
      href="#"
      className={tileSize == Tile.sizes.table ? "active" : ""}
      onClick={e => setTileSize(Tile.sizes.table)}>
        <span className="glyphicon glyphicon-th-list" />
    </a>
  </li>
)

export default connect(mapStateToProps, mapDispatchToProps)(TileSizeSelector)
