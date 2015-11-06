var RoleOverview = React.createClass({

  getInitialState() {
    return {
      filter: "",
      roles: []
    };
  },

  componentDidMount() {
    this.getRoles();
  },

  getRoles() {

    $.ajax({
      url: "/api/roles/all",
      dataType: 'json',
      cache: false,
      success: function(data) {
        this.setState({
          roles: data || []
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

    var roles = this.state.roles
      .filter(function(role) {
        return role.name.search(filter) != -1;
      })
      .map(function(role, index) {
        return (
          <RoleTile key={index} role={role} />
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
            {roles}
        </div>
      </div>
    );

  }

});
