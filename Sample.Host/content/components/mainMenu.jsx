var MainMenu = React.createClass({

  onTabSelect(key) {
    this.props.navigate(key);
  },

  setSmallGrid(e) {
    e.preventDefault();
    this.props.setTileSize(Tile.sizes.small);
  },

  setMediumGrid(e) {
    e.preventDefault();
    this.props.setTileSize(Tile.sizes.large);
  },

  render() {

    var navigate = this.props.navigate;
    var selected = this.props.selected;

    var items = [ "Users", "Roles", "Permissions" ];

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
          <a href="#" onClick={this.setSmallGrid}><span className="glyphicon glyphicon-th" /></a>
          <a href="#" onClick={this.setMediumGrid}><span className="glyphicon glyphicon-th-large" /></a>
        </li>
      </ul>
    );
  }

});
