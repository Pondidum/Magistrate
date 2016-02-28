import React from 'react'
import Tile from './tile'

var MainMenu = React.createClass({

  onTabSelect(key) {
    this.props.navigate(key);
  },

  setSmallGrid(e) {
    e.preventDefault();
    this.props.setTileSize(Tile.sizes.small);
  },

  setLargeGrid(e) {
    e.preventDefault();
    this.props.setTileSize(Tile.sizes.large);
  },

  setTableGrid(e) {
    e.preventDefault();
    this.props.setTileSize(Tile.sizes.table);
  },

  render() {

    var navigate = this.props.navigate;
    var selected = this.props.selected;
    var tileSize = this.props.tileSize;

    var items = [ "Users", "Roles", "Permissions", "History" ];

    var tabs = items.map(function(item, index) {

      var onClick = function(e) {
        e.preventDefault();
        navigate(item.toLowerCase());
      }

      var active = selected == item.toLowerCase() ? "active" : "";

      return (
        <li key={index} className={active}>
          <a href="#" onClick={onClick}>{item}</a>
        </li>
      );

    });

    return (
      <ul className="nav nav-tabs">
        {tabs}
        <li className="pull-right">
          <a href="#" className={tileSize == Tile.sizes.small ? "active" : ""} onClick={this.setSmallGrid}>
            <span className="glyphicon glyphicon-th" />
          </a>
          <a href="#" className={tileSize == Tile.sizes.large ? "active" : ""} onClick={this.setLargeGrid}>
            <span className="glyphicon glyphicon-th-large" />
          </a>
          <a href="#" className={tileSize == Tile.sizes.table ? "active" : ""} onClick={this.setTableGrid}>
            <span className="glyphicon glyphicon-th-list" />
          </a>
        </li>
      </ul>
    );
  }

});

export default MainMenu
