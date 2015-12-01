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

  onFilterChanged(value) {
    this.setState({
      filter: value
    });
  },

  onCreate(role) {
    var newCollection = this.state.roles.concat([role]);

    this.setState({
      roles: newCollection
    });
  },

  render() {

    var self = this;
    var filter = new RegExp(this.state.filter, "i");

    var roles = this.state.roles
      .filter(function(role) {
        return role.name.search(filter) != -1;
      })
      .map(function(role, index) {
        return (
          <RoleTile
            key={index}
            role={role}
            onRoleRemoved={self.onRoleRemoved}
            navigate={self.props.navigate}
          />
        );
      });

    var create = (<CreateRoleDialog onCreate={this.onCreate} />);

    return (
      <div>
        <ContentArea filterChanged={this.onFilterChanged} actions={[create]}>
          {roles}
        </ContentArea>
      </div>
    );

  }

});
