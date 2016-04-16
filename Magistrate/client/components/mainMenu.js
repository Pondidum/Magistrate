import React from 'react'
import { connect } from 'react-redux'
import { routeActions } from 'react-router-redux'

import Tile from './tile'
import TileSizeSelector from './TileSizeSelector'

const mapDispatchToProps = (dispatch) => {
  return {
    navigate: (area) => {
      dispatch(routeActions.push("/" + area + "/"));
    }
  }
}


const MainMenu = ({ route, navigate }) => {

  const items = [ "Users", "Roles", "Permissions", "History" ];

  const tabs = items.map(function(item, index) {

    var onClick = function(e) {
      e.preventDefault();
      navigate(item.toLowerCase());
    }

    var active = route && route[0].path.toLowerCase() == item.toLowerCase() ? "active" : "";

    return (
      <li key={index} className={active}>
        <a href="#" onClick={onClick}>{item}</a>
      </li>
    );

  });

  return (
    <ul className="nav nav-tabs">
      {tabs}
      <TileSizeSelector className="pull-right" />
    </ul>
  );
}

export default connect(null, mapDispatchToProps)(MainMenu)
