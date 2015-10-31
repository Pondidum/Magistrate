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

  render() {
    var user = this.props.user;
    var checked = this.state.checked;

    var styleName = checked ? "panel panel-info" : "panel panel-default";

    return (
      <div className="col-md-3" onClick={this.onClick}>
        <div className={styleName}>
          <div className="panel-heading">
            <h3 className="panel-title">{user.name}<span className="glyphicon glyphicon-remove-circle pull-right"></span></h3>
          </div>
          <div className="panel-body">
            <div>Roles: {user.roles.lenth || 0}</div>
            <div>Includes: {user.includes.lenth || 0}</div>
            <div>Revokes: {user.revokes.lenth || 0}</div>
          </div>
        </div>
      </div>
    );
  }
})
