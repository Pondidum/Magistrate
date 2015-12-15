var MainMenu = React.createClass({

  onTabSelect(key) {
    this.props.navigate(key);
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
      </ul>
    );
  }

});
