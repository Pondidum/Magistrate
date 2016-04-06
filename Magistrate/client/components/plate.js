import React from 'react'
import { connect } from 'react-redux'

import Tile from './tile'

const mapStateToProps = (state, ownProps) => {
  return {
    tileSize: state.ui.tileSize,
    ...ownProps
  }
}

const PlateHeader = ({ title, onCross }) => (
  <div className="panel-heading">
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
const PlateLarge = ({ title, onCross, tileSize, children }) => (
  <li className="col-md-3 tile">
    <div className="panel panel-default">
      <PlateHeader title={title} onCross={onCross} />
      <div className="panel-body" style={{ height: "100px" }}>
        <PlateChildren tileSize={tileSize} children={children} />
      </div>
    </div>
  </li>
);

const PlateSmall = ({ title, onCross }) => (
  <li className="col-md-3 tile">
    <div className="panel panel-default">
      <PlateHeader title={title} onCross={onCross} />
    </div>
  </li>
)

const PlateRow = ({ title, onCross, tileSize, children }) => (
  <li className="col-md-12 tile">
    <div className="row panel panel-default">
      <div className="panel-body">
        <div className="col-sm-3">{title}</div>
        <div className="col-sm-8"><PlateChildren tileSize={tileSize} children={children} /></div>
        <div className="col-sm-1">
          <a href="#" onClick={onCross}>
            <span className="glyphicon glyphicon-remove-circle"></span>
          </a>
        </div>
      </div>
    </div>
  </li>
)

const Plate = (props) => {

  const tileSize = props.tileSize;

  if (tileSize == Tile.sizes.large)
    return <PlateLarge {...props} />

  if (tileSize == Tile.sizes.table)
    return <PlateRow {...props} />

  return <PlateSmall {...props} />
}

export default connect(mapStateToProps)(Plate)
