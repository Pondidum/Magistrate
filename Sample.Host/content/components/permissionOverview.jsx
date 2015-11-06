var PermissionOverview = React.createClass({

  getInitialState() {
    return {
      filter: "",
      permissions: []
    };
  },

  componentDidMount() {
    this.getPermissions();
  },

  getPermissions() {

    $.ajax({
      url: "/api/permissions/all",
      dataType: 'json',
      cache: false,
      success: function(data) {
        this.setState({
          permissions: data || []
        });
      }.bind(this),
      error: function(xhr, status, err) {
        console.error(this.props.url, status, err.toString());
      }.bind(this)
    });

  },

  filterChanged(value) {
    this.setState({
      filter: value
    });
  },

  render() {

    var filter = new RegExp(this.state.filter, "i");

    var permissions = this.state.permissions
      .filter(function(permission) {
        return permission.name.search(filter) != -1;
      })
      .map(function(permission, index) {
        return (
          <div key={index} className="col-md-3">
            <div className="panel panel-default">
              <div className="panel-heading">
                <h3 className="panel-title">
                  {permission.name}
                </h3>
              </div>
              <div className="panel-body">
                <p>{permission.description}</p>
              </div>
            </div>
          </div>
        );
      });

    return (
      <div>
        <div className="row" style={{ marginBottom: "1em" }}>
          <div className="col-md-7">

            <ul className="list-unstyled list-inline">
            </ul>

          </div>
          <FilterBar className="pull-right col-md-5" filterChanged={this.filterChanged} />
        </div>
        <div className="row">
            {permissions}
        </div>
      </div>
    );

  }

});
