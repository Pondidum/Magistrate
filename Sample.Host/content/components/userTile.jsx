var UserTile = React.createClass({

  getInitialState() {
    return {
      checked: false
    };
  },

  onClick() {
    this.setState({
      checked: !this.state.checked
    });
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
            <div>Permissions: 15</div>
            <div>Roles: 3</div>
          </div>
        </div>
      </div>
    );
  }
})
