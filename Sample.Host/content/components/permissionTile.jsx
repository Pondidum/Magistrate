var PermissionTile = React.createClass({

  render() {

    var permission = this.props.permission;

    return (
      <div>
        <div className="panel panel-default">
          <div className="panel-heading">
            <h3 className="panel-title">
              {permission.name}
            </h3>
          </div>
          <div className="panel-body" style={{ height: "100px" }}>
            <p>{permission.description}</p>
          </div>
        </div>
      </div>
    );
  }

});
