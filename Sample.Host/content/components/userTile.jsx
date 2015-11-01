var UserTile = React.createClass({

  getInitialState() {
    return {
      checked: false
    };
  },

  onClick() {
    var newState = !this.state.checked;

    this.setState({
      checked: newState
    });

    this.props.onChange(newState);
  },

  navigateToDetails(e) {
    e.preventDefault();
    this.props.navigate("singleuser", { key: this.props.user.key});
  },

  render() {
    var user = this.props.user;
    var checked = this.state.checked;

    var styleName = checked ? "panel panel-info" : "panel panel-default";

    return (
      <div className="col-md-3" onClick={this.onClick}>
        <div className={styleName}>
          <div className="panel-heading">
            <h3 className="panel-title">
              <a onClick={this.navigateToDetails} href="#">
                {user.name}
              </a>
              <span className="glyphicon glyphicon-remove-circle pull-right"></span></h3>
          </div>
          <div className="panel-body">
            <div>Roles: {user.roles.length}</div>
            <div>Includes: {user.includes.length}</div>
            <div>Revokes: {user.revokes.length}</div>
          </div>
        </div>
      </div>
    );
  }
})
