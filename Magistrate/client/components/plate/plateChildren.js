import React from 'react'
import Tile from '../tile'

const PlateChildren = ({ children, tileSize }) => {
  if (children.constructor !== Array) {
    return (<span>{children}</span>);
  }

  var list = children.map(function(child, index) {
    return (<li key={index}>{child}</li>);
  });

  return (
    <ul className={"list-unstyled " + (tileSize == Tile.sizes.table ? "list-inline" : "")}>
      {list}
    </ul>
  )
}

export default PlateChildren
