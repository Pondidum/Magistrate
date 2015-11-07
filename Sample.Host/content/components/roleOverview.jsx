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

  onRoleCreated(role) {
    var newCollection = this.state.roles.concat([role]);

    this.setState({
      roles: newCollection
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
        <OverviewActionBar filterChanged={this.filterChanged}>
          <li><CreateRoleDialog onRoleCreated={this.onRoleCreated} /></li>
        </OverviewActionBar>
        <div className="row">
            {roles}
        </div>
      </div>
    );

  }

});
