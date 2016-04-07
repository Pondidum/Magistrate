import React from 'react'
import PlateSizes from './sizes'

const PlateChildren = ({ children, tileSize }) => {
  if (children.constructor !== Array) {
    return (<span>{children}</span>);
  }

  var list = children.map(function(child, index) {
    return (<li key={index}>{child}</li>);
  });

  return (
    <ul className={"list-unstyled " + (tileSize == PlateSizes.table ? "list-inline" : "")}>
      {list}
    </ul>
  )
}

export default PlateChildren
