var RoleTile = React.createClass({

  render() {

    var role = this.props.role;

    return (
      <div className="col-md-3">
        <div className="panel panel-default">
          <div className="panel-heading">
            <h3 className="panel-title">
              {role.name}
            </h3>
          </div>
          <div className="panel-body">
            <p>{role.description}</p>
          </div>
        </div>
      </div>
    );
  }

});
