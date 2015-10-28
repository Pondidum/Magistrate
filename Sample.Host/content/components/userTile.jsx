var UserTile = React.createClass({

  render() {
    var user = this.props.user;

    return (
      <div className="col-md-3">
        <div className="panel panel-default">
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
