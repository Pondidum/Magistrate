var Tabs = ReactBootstrap.Tabs;
var Tab = ReactBootstrap.Tab;

var MainMenu = React.createClass({

  onTabSelect(key) {
    this.props.navigate(key);
  },

  render() {

    var selected = this.props.selected;

    return (
      <Tabs activeKey={this.props.selected} onSelect={this.onTabSelect}>
        <Tab eventKey="users" title="Users" />
        <Tab eventKey="roles" title="Roles" />
        <Tab eventKey="permissions" title="Permissions" />
      </Tabs>
    );
  }

});
